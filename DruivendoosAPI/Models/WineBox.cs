namespace DruivendoosAPI.Models
{
    public class WineBox
    {
        public int BoxId { get; set; }
        public int WineId { get; set; }
        public Box Box { get; set; }
        public Wine Wine { get; set; }

        public WineBox()
        {

        }

        public WineBox(Box doos, Wine wijn)
        {
            Box = doos;
            BoxId = Box.Id;
            Wine = wijn;
            WineId = Wine.WineId;
        }
    }
}
