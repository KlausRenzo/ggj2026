using System;
using System.Collections.Generic;

public static class ServiceManager
{
	private static Dictionary<Type, object> services = new();

	public static void RegisterService<T>(T instance) => services.Add(typeof(T), instance);

	public static T GetService<T>() => (T) services[typeof(T)];
}