using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnBoardingTask2018Jun.Models.New_Models
{
    public enum ActionStates : int
    {
        Initial = 0,
        IndexGet = 1,
        CreateGet = 2,
        CreatePost = 3,
        CreateInputInvalid = 4,
        EditGet = 5,
        EditPost = 6,
        EditInputInvalid = 7,
        DeleteGet = 8,
        DeletePost = 9
    }
}