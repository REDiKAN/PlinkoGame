using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intefaces
{
    public interface IScoreAdder{
        int MoneyToAdd { get; set; }
        Action Action { get; set; }
    }
}
