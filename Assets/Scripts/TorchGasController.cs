﻿using System;

public static class TorchGasController
{
    public static Action<int> OnChange;

    public static void ChangeTorchGas(int value)
    {
        OnChange?.Invoke(value);
    }
}