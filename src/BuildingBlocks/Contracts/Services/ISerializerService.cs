﻿namespace BuildingBlocks.Contracts.Services;
public interface ISerializerService
{
    string Serialize<T>(T obj);
    T Deserialize<T>(string text);
}
