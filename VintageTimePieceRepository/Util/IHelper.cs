﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimePieceRepository.Util
{
    public interface IHelper
    {
        public Task<string> UploadImageToFirebase(string imageBase64);
    }
}