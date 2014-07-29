﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Abstractions.Data;
using Raven.Client;

namespace FurryBear
{
    public static class Extensions
    {
        public static bool TryGetServerVersion(this IDocumentStore documentStore, out BuildNumber buildNumber, int timeoutMilliseconds = 5000)
        {
            try
            {
                var task = documentStore.AsyncDatabaseCommands.GetBuildNumberAsync();
                var success = task.Wait(timeoutMilliseconds);
                buildNumber = task.Result;
                return success;
            }
            catch
            {
                buildNumber = null;
                return false;
            }
        }

        public static bool IsServerOnline(this IDocumentStore documentStore, int timeoutMilliseconds = 5000)
        {
            BuildNumber buildNumber;
            return documentStore.TryGetServerVersion(out buildNumber, timeoutMilliseconds);
        }
    }
}