﻿using System.Windows.Input;

namespace Hirundo.Commons.WPF;

public interface IRemovable
{
    ICommand RemoveCommand { get; }
    public event EventHandler<ParametersEventArgs>? Removed;
}

public interface IRemovable<T>
{
    ICommand RemoveCommand { get; }
    event EventHandler<T> Removed;
}