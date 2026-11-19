#!/usr/bin/env bash
# ============================================================
#  Backup de la base bd_GranET con mysqldump (Linux)
#  Pide usuario y contraseña de MySQL al ejecutarse.
# ============================================================
#
#  Uso:
#    chmod +x backup_granet.sh
#    ./backup_granet.sh
#
#  Uso sin preguntas (por ejemplo para pruebas o scripts):
#    DB_USER=backup_user DB_PASS='mi_password' ./backup_granet.sh
#
#  Nota: como pide datos por teclado, NO sirve tal cual para cron.
# ============================================================

set -euo pipefail

# ---------- CONFIGURACION ----------
DB_NAME="bd_GranET"
DB_HOST="localhost"
BACKUP_DIR="${HOME}/backups_granet"
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

# Pedir usuario y contraseña si no vinieron como variables de entorno
if [[ -z "${DB_USER:-}" ]]; then
    read -r -p "Usuario de MySQL [backup_user]: " DB_USER
    DB_USER="${DB_USER:-backup_user}"
fi

if [[ -z "${DB_PASS:-}" ]]; then
    read -r -s -p "Contraseña de ${DB_USER}: " DB_PASS
    echo
fi

if [[ -z "$DB_PASS" ]]; then
    echo "ERROR: la contraseña no puede estar vacia." >&2
    exit 1
fi

mkdir -p "$BACKUP_DIR"
chmod 700 "$BACKUP_DIR"

# Si algo falla, borrar el archivo parcial
trap 'rm -f "$TMP_FILE"' ERR

log "Iniciando backup de ${DB_NAME} (usuario: ${DB_USER})..."

# La contraseña se pasa por variable de entorno solo a mysqldump,
# asi no queda visible en la lista de procesos ni en el historial.
# --databases incluye CREATE DATABASE y USE, asi que el restore es directo.
# --single-transaction: backup consistente sin bloquear tablas InnoDB.
# utf8mb4: necesario porque hay una columna con tilde (cotización).
MYSQL_PWD="$DB_PASS" mysqldump \
    --host="$DB_HOST" \
    --user="$DB_USER" \
    --single-transaction \
    --no-tablespaces \
    --routines --triggers --events \
    --default-character-set=utf8mb4 \
    --databases "$DB_NAME" \
    | gzip -9 > "$TMP_FILE"

unset DB_PASS

# Verificar que el gzip sea valido antes de darlo por bueno
gzip -t "$TMP_FILE"
mv "$TMP_FILE" "$OUT_FILE"
chmod 600 "$OUT_FILE"

SIZE="$(du -h "$OUT_FILE" | cut -f1)"
log "Backup OK: ${OUT_FILE} (${SIZE})"

# Borrar backups mas viejos que RETENTION_DAYS
DELETED="$(find "$BACKUP_DIR" -name "${DB_NAME}_*.sql.gz" -type f -mtime +"$RETENTION_DAYS" -print -delete | wc -l)"
log "Backups antiguos eliminados: ${DELETED}"