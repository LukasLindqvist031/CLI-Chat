﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI_Chat.Server.Models
{
    public class Message
    {
        public User Sender { get; set; }
        public string Content { get; set; }
    }
}
