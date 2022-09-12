
public class PhotonStatus
{
    private string playName;
    private int status;
    private string message;

    public string PlayerName { get => playName; set => playName = value; }
    public int Status { get => status; set => status = value; }
    public string Message { get => message; set => message = value; }

    public PhotonStatus(string playerNam, int status, string message)
    {
        playName = playerNam;
        this.status = status;
        this.message = message;
    }
}
 