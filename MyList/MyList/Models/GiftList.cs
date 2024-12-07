namespace MyList.Models;

public class GiftList
{
    public int Id { get; set; }
    public string ListName { get; set; }
    public string GiftName { get; set; }
    public string GiftDescription { get; set; }
    public bool IsPurchased { get; set; }
}