using System;

namespace MfaCsvUploader.Models
{
    public class HomeViewModel
    {
        public string Message { get; set; }
        public bool Succcess { get; set; }
        public string AlertType => Succcess ? "success" : "danger"; 
    }
}