﻿using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services
{
    [ExportTransientAs(nameof(IBrewProcessService))]
    public class BrewProcessService : IBrewProcessService
    {

        IDispatcherTimer timer;
        DateTime time;
        const int timerTickRate = 50; // miliseconds

        Action<TimeSpan>? tickCallback;
        public BrewProcessService()
        {
            if (Application.Current == null) throw new ArgumentException("Application cannot be null");

            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(0, 0,0 ,0, timerTickRate);
            timer.Tick += new EventHandler(TimerTick);
        }

        private void TimerTick(object? sender, EventArgs? e)
        {
            if (tickCallback == null) return;
            tickCallback.Invoke(DateTime.Now.Subtract(time));
        }

        public void RegisterTickCallback(Action<TimeSpan> callback)
        {
            tickCallback = callback;
        }

        public void Start()
        {
            timer.Start();
            time = DateTime.Now; 
        }
        public void Stop() { 
            timer.Stop(); 
        }


    }
}

