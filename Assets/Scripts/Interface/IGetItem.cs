// hocam bazı araştırmalara göre çok kullanacagımız enum'ları ayrı bir dosyada tutmak daha iyi olur.
public enum ItemType
{
    TOMATO, LETTUCE, ONION, CHEESE, MEATBALL, BREAD, NONE,
    SLICEDTOM, SLICEDLET, SLICEDON, SLICEDCHE, COOKEDMEAT, SLICEDBREAD, PLATE, SlICEDBREADTOP, HAMBURGER, BURNEDMEAT
}
public interface IGetItem
{
    public ItemType GetItem();
} 
