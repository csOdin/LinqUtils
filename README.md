
# LinqUtils
LinqUtils provide utilities to easily create Linq expressions from onject properties or strings

### Sample data
All following examples are based on the followin model
``` csharp
    public class Address
    {
        public string City { get; set; }
        public Guid Id { get; } = Guid.NewGuid();
        public string Street { get; set; }
    }
    
    public class Person
    {
        public Address Address { get; set; }
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Surname { get; set; }
    }
```

## Filters
Create conditions based on your class properties and get Linq expression to apply in the Where clause of your Linq command.

### Conditions
 - ExpressionCondition
 - ContainsCondition
 - EqualsCondition
 - StartsWithCondition
 - EndsWithCondition
 - GreaterThanCondition
 - GreaterThanOrEqualsCondition
 - LessThanCondition
 - LessThanOrEqualsCondition

 Required  Using statement
 ``` csharp
	Using csOdin.LinqUtils.Filters;
```

#### from Lambda expression
``` csharp
	var filter = ContainsCondition<Person>.Create(p => p.Name, "searchedValue");
	var filteredlist = myList.Where(filter);
```
#### from string
``` csharp
	var filter = ContainsCondition<Person>.Create("Name", "searchedValue");
	var filteredlist = myList.Where(filter);
```
#### from expression
``` csharp
	var filter = new ExpressionCondition<Person>.Create(o => o.Name.Contains("searchedValue"));
	var filteredlist = myList.Where(filter);
```
### And Clause	
Creates an And clause that includes all given conditions
``` csharp
	var people = new List<Person>();
	// Populate people
	var condition1 = ContainsCondition<Person>.Create(p => p.Name, "searchedValue1");
	var condition1 = ContainsCondition<Person>.Create(p => p.Address.City, "searchedValue2");
	
	var andClause = new AndClause();
	andClause.Add(condition1, condition2);

	var filteredlist = myList.Where(andClause);
```
### Or Clause	
Creates an Or clause that includes all given conditions
``` csharp
	var people = new List<Person>();
	// Populate people
	var condition1 = ContainsCondition<Person>.Create(p => p.Name, "searchedValue1");
	var condition2 = ContainsCondition<Person>.Create(p => p.Name, "searchedValue2");
	
	var orClause = new OrClause();
	orClause.Add(condition1, condition2);

	var filteredlist = myList.Where(orClause);
```	
### Complex And/Or clauses
Combinations of nested And / Or clauses can be created
Given 3 conditions :
``` csharp
	var people = new List<Person>();
	// Populate people
	var condition1 = ContainsCondition<Person>.Create(p => p.Name, "searchedValue1");
	var condition2 = ContainsCondition<Person>.Create(p => p.Name, "searchedValue2");
	var condition3 = ContainsCondition<Person>.Create(p => p.Address.City, "searchedValue3");
```	
**And with nested Or**
```csharp
	var orClause = new OrClause();
	orClause.Add(condition1, condition2);

	var andClause = new andClause();
	andClause.Add(condition1);
	andclause.Add(orClause);
	
	var filteredlist = myList.Where(andclause);
```
**Or with nested And**
```csharp
	var andClause = new AndClause();
	AndClause.Add(condition1, condition2);

	var orClause = new orClause();
	andClause.Add(condition1);
	andclause.Add(andClause);
	
	var filteredlist = myList.Where(orClause);
```
## OrderBy
Create order by clauses based on your class properties or from SQL order by strig
 ``` csharp
	Using csOdin.LinqUtils.OrderBy;
```

#### from string
```csharp
	var orderByClause = "Address.City, Name desc";

	var orderedList = myList.OrderBy(orderByClause);
```
#### from properties
```csharp
	Using csOdin.LinqUtils.OrderBy.Clauses;

    var orderByClause = OrderByClause<Person>.Create()
		.AddAscending(o => o.Name)
        .AddDescending("Address.City")
        .AddAscending(o => o.IntProperty);

	var orderedList = myList.OrderBy(orderByClause);
```
