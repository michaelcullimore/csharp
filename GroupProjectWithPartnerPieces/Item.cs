using System;

namespace FinalProject
{
    /// <summary>
    /// This class represents an item.
    /// Each item has a code, description, cost and boolean to determine whether the item is being used in an invoice or not
    /// </summary>
    class Item
    {
        public String Item_Code { get; set; }
        public String Item_Description{ get; set; }
        public String Item_Cost{ get; set; }
        public Boolean itemIsUsed { get; set; }

        /// <summary>
        /// Constructor that requires setting values for each property of an item.
        /// A newly created item by default will be considered "not being used"
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        public Item(String code, String desc, String cost)
        {
            Item_Code = code;
            Item_Description = desc;
            Item_Cost = cost;
            itemIsUsed = false;
        }

        /// <summary>
        /// Constructor that creates an empty item
        /// </summary>
        public Item()
        {}
    }
}
