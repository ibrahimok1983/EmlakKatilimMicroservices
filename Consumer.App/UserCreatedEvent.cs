namespace Consumer.App
{
    internal class UserCreatedEvent
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
