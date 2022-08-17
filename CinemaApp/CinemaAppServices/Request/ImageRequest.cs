﻿using CinemaAppServices.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request
{
    public class ImageRequest
    {
        public ImageRequest()
        {
        }
        public ImageRequest(string name, string fileType, string extension, DateTime createdOn, byte[] data)
        {
            Name = name;
            FileType = fileType;
            Extension = extension;
            CreatedOn = createdOn;
            Data = data;
        }

        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Data { get; set; }
    }
}
