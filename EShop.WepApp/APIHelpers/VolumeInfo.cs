using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WepApp.APIHelpers
{
    public class VolumeInfo
    {
        public ImageLinks imageLinks { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public List<string> authors { get; set; }
    }
}
