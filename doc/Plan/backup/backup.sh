#!/usr/bin/env bash
# ============================================================
#  Backup de la base bd_GranET con mysqldump (Linux)
# ============================================================
#
#  Uso:
#    chmod +x backup_granet.sh
#    ./backup_granet.sh
#
#  Credenciales: crear el archivo ~/.granet_backup.cnf (chmod 600) con:
#
#    [client]
#    user=backup_user
#    password=TU_PASSWORD
#    host=localhost
#
#  Cron (todos los dias a las 3:00 AM):
#    0 3 * * * /ruta/a/backup_granet.sh >> /var/log/granet_backup_cron.log 2>&1
# ============================================================

set -euo pipefail

# ---------- CONFIGURACION ----------
DB_NAME="bd_GranET"
CREDENTIALS_FILE="${HOME}/.granet_backup.cnf"
BACKUP_DIR="/var/backups/mysql/${DB_NAME}"
RETENTION_DAYS=7
# -----------------------------------

TS="$(date +%Y%m%d_%H%M%S)"
OUT_FILE="${BACKUP_DIR}/${DB_NAME}_${TS}.sql.gz"
TMP_FILE="${OUT_FILE}.partial"
LOG_FILE="${BACKUP_DIR}/backup_log.txt"

log() {
    echo "[$(date '+%Y-%m-%d %H:%M:%S')] $*" | tee -a "$LOG_FILE"
}

# Verificaciones previas
command -v mysqldump >/dev/null 2>&1 || { echo "ERROR: mysqldump no esta instalado." >&2; exit 1; }
command -v gzip      >/dev/null 2>&1 || { echo "ERROR: gzip no esta instalado." >&2; exit 1; }

if [[ ! -f "$CREDENTIALS_FILE" ]]; then
    echo "ERROR: no existe el archivo de credenciales: $CREDENTIALS_FILE" >&2
    exit 1
fi

mkdir -p "$BACKUP_DIR"
chmod 700 "$BACKUP_DIR"

# Si algo falla, borrar el archivo parcial
trap 'rm -f "$TMP_FILE"' ERR

log "Iniciando backup de ${DB_NAME}..."

# --databases incluye CREATE DATABASE y USE, asi que el restore es directo.
# --single-transaction: backup consistente sin bloquear tablas InnoDB.
# utf8mb4: necesario porque hay una columna con tilde (cotización).
mysqldump \
    --defaults-extra-file="$CREDENTIALS_FILE" \
    --single-transaction \
    --routines --triggers --events \
    --default-character-set=utf8mb4 \
    --databases "$DB_NAME" \
    | gzip -9 > "$TMP_FILE"

# Verificar que el gzip sea valido antes de darlo por bueno
gzip -t "$TMP_FILE"
mv "$TMP_FILE" "$OUT_FILE"
chmod 600 "$OUT_FILE"

SIZE="$(du -h "$OUT_FILE" | cut -f1)"
log "Backup OK: ${OUT_FILE} (${SIZE})"

# Borrar backups mas viejos que RETENTION_DAYS
DELETED="$(find "$BACKUP_DIR" -name "${DB_NAME}_*.sql.gz" -type f -mtime +"$RETENTION_DAYS" -print -delete | wc -l)"
log "Backups antiguos eliminados: ${DELETED}"