﻿using System;
using MediatR;

namespace MediatorPattern.Notifications
{
    public class ErroNotification : INotification
    {
        public string Erro { get; set; }
        public string PilhaErro { get; set; }
    }
}