
public class ChatMessage
{
    private string goal;
    private string message;

    public string Goal { get => goal; set => goal = value; }
    public string Message { get => message; set => message = value; }

    public ChatMessage(string goal, string message)
    {
        this.goal = goal;
        this.message = message;
    }
}
