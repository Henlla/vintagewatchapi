﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IImageService
    {
        public Task<APIResponse<string>> uploadImage(IFormFile file, string folder);
        public Task<APIResponse<TimepieceImage>> CreateNewTimepieceImage(TimepieceImage timepieceImage);
    }
}
