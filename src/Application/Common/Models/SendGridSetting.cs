﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Common.Models;
public class SendGridSettings
{
    public string ApiKey { get; set; } = "";
    public string SenderEmail { get; set; } = "";
    public string SenderName { get; set; } = "";
}
