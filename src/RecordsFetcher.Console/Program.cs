using Microsoft.Extensions.DependencyInjection;
using System;
using RecordsFetcher.Application;
using RecordsFetcher.Application.RemoteDataService;
using RecordsFetcher.Application.CachingService;
using RemoteServer.Models;
using RemoteServer.Services;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IRemoteDataService, RemoteDataService>();
        services.AddSingleton<ICachingService, InMemoryCachingService>();
        services.AddSingleton<PagingService>();
        services.AddSingleton<RemoteRecordRetriever>();

        var serviceProvider = services.BuildServiceProvider();
        
        // Store records and perform operations
        var pagingService = serviceProvider.GetService<PagingService>();

        PopulateRecords(serviceProvider);

        DisplayRecords(pagingService.GetRecords(1, 5));
        DisplayRecords(pagingService.GetRecords(2, 5));
        DisplayRecords(pagingService.GetRecords(3, 5));
    }

    private static void PopulateRecords(ServiceProvider serviceProvider)
    {
        var records = new DataRecord[300];
        for (int i = 0; i < 300; i++)
        {
            records[i] = new DataRecord
            {
                ID = i + 1,
                CreationDate = ServerDateTime.addMilliseconds(ServerDateTime.getCurrentTime(), -100000 * i),
                Data = Array.Empty<byte>()
            };
        }

        var remoteRecordRetriever = serviceProvider.GetService<RemoteRecordRetriever>();
        remoteRecordRetriever!.StoreRecords(records);
    }

    private static void DisplayRecords(DataRecord[] records)
    {
        Console.WriteLine($"Retrieved {records.Length} records:");
        foreach (var record in records)
        {
            Console.WriteLine($"Record ID: {record.ID}, Creation Date: {record.CreationDate}");
        }
        Console.WriteLine();
    }
}