namespace ConsoleAppQuickTest
{
    public class AzdTicket
    {
        public string AzdId { get; set; }

        public string JiraId { get; set; }

        public string JiraInternalDevId { get; set; }

        public override string ToString()
        {
            return $"AzdId {this.AzdId} | JiraId {this.JiraId} | JiraInternalDevId {this.JiraInternalDevId}";
        }
    }
}
