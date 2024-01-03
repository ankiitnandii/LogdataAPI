namespace LogdataAPI.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string Operationname { get; set; }
        public string Status { get; set; }

        public string Eventcategory { get; set; }

        public string Resourcetype { get; set; }

        public string Resource { get; set; }

    }
}
