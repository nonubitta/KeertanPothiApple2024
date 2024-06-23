using System;
using System.Collections.Generic;
using System.Text;

namespace KeertanPothi.model
{
    public class SideMenu
    {
        public SideMenu(string itemName, string viewName, bool itemEnabled = true, string imageUrl = null, bool isVisible = true, int fontSize = 18)
        {
            ItemName = itemName;
            //PunjabiName = punjabiName;
            ImageUrl = imageUrl;
            ViewName = viewName;
            ItemEnabled = itemEnabled;
            IsVisible = isVisible;
            FontSize = fontSize;
        }
        public string ItemName { get; set; }
        public string PunjabiName { get; set; }
        public string ViewName { get; set; }
        public string ImageUrl { get; set; }
        public bool ItemEnabled { get; set; }
        public bool IsVisible { get; set; }
        public int FontSize { get; set; }
    }
}
