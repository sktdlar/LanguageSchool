using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Components
{
    partial class ClientService
    {
            public string RemaningTime
            {
                get
                {
                    int time = (int)(StartTime - DateTime.Now).TotalMinutes;
                    return $"Часов:{time / 60} минут:{time % 60}";

                }
            }
            public string ColorTime
            {
                get
                {
                    int time = (int)(StartTime - DateTime.Now).TotalMinutes;
                    if (time < 60)
                    {
                        return "Red";
                    }
                    else
                    {
                        return "Black";
                    }
                }
            }
        }
    }
