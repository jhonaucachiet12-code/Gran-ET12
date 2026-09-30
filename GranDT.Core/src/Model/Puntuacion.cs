namespace GranDT.Core.Model;

public class Puntuacion
{
    public short IdJugador { get; set; }
    public byte Fecha { get; set; }
    public decimal Puntuaciones { get; set; }

    public Jugador? jugador { get; set; }
}
