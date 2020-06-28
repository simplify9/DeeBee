[![Build Status](https://dev.azure.com/simplify9/Github%20Pipelines/_apis/build/status/simplify9.DeeBee?branchName=master)](https://dev.azure.com/simplify9/Github%20Pipelines/_build/latest?definitionId=168&branchName=master) 

![Azure DevOps tests](https://img.shields.io/azure-devops/tests/Simplify9/Github%20Pipelines/168?style=for-the-badge)


| **Package**       | **Version** |
| :----------------:|:----------------------:|
|```SimplyWorks.DeeBee```| ![Nuget](https://img.shields.io/nuget/v/SimplyWorks.DeeBee?style=for-the-badge)


## Introduction 
*DeeBee* is a library providing Object Relational Mapping (ORM) support to applications, libraries, and frameworks. 

*DeeBee* specializes in abstracting away from the chore of writing in SQL for database management processes. These abstractions are provided by extension methods over the [DbContext Class](https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0), making *DeeBee* the ideal tool to use for projects with basic CRUD requirements. It does not require much ceremony and set up to start using!


## Installation 
There are two [NuGet](https://www.nuget.org/packages/SimplyWorks.DeeBee/) packages for *DeeBee*, one being for the service, installed with:
```csharp
dotnet add package SimplyWorks.DeeBee 
```
While the other is used to integrate it into the dependency injection, with: 
```csharp
dotnet add package SimplyWorks.DeeBee.Extensions
```


## Examples
The following examples show *DeeBee* in action. 

// Getting a single record from database
```var parcels = await db.SingleOrDefault<Parcel>(
    new List<SearchyCondition> { new SearchyCondition(nameof(Parcel.Id),
    SearchyRule.EqualsTo, Id) });
```

// Getting "all" (max 1000) records from database
``` var parcels = await db.All<Parcel>(
    new List<SearchyCondition> { new SearchyCondition(nameof(Parcel.ItemCount),
    SearchyRule.GreaterThan, 2) });
```

```namespace Project.Resources.Suppliers
{
    public class Search : ISearchyHandler
    {
        private readonly DbContext db;

        public Search(DbContext db)
        {
            this.db = db;
        }
        // Interacts smoothly with searchyRequest from primitive types & CqApi handlers
        async public Task<object> Handle(SearchyRequest searchyRequest, bool lookup = true, string searchPhrase = null)
        {

            var results = await db.All<Agent>(
               searchyRequest.Conditions,
               searchyRequest.Sorts)
            });

            return results;
        }
    }
}
```


## Getting support 👷
If you encounter any bugs, don't hesitate to submit an [issue](https://github.com/simplify9/DeeBee/issues). We'll get back to you promptly!
