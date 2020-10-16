# LinqUtils
LinqUtils provide utilities to easily create Linq expressions from onject properties or strings

## Filters
Create conditions based on your class properties and get Linq expression to apply in the Where clause of your Linq command.

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

### Conditions
#### from Lambda expression
``` csharp
	var condition = new Condition<Person>(p => p.Name, FilterOperators.Contains, "searchedValue");
	var filter = condition.ToLinq();
	// filter = o => o.Name.Contains("searchedValue");
```
#### from string
``` csharp
	var condition = new Condition<Person>("Name", FilterOperators.Contains, "searchedValue");
	var filter = condition.ToLinq();
	// filter = o => o.Name.Contains("searchedValue");	
```
#### from expression
``` csharp
	var condition = new Condition<Person>(o => o.Name.Contains("searchedValue"));
	var filter = condition.ToLinq();
	// filter = o => o.Name.Contains("searchedValue");	
```
### And Clause	
Creates an And clause that includes all given conditions
``` csharp
	var people = new List<Person>();
	// Populate people
	var condition1 = new Condition<Person>(p => p.Name, FilterOperators.Contains, "searchedValue1");
	var condition1 = new Condition<Person>(p => p.Address.City, FilterOperators.Contains, "searchedValue2");
	
	var andClause = new AndClause();
	andClause.Add(condition1, condition2);
	filter = andClause.ToLinq();
	// returns confition1 && condition2
	// filter = o => o.Name.Contains("searchedValue1") && o.Address.City.Contains("searchedValue2")
```
### Or Clause	
Creates an Or clause that includes all given conditions
``` csharp
	var people = new List<Person>();
	// Populate people
	var condition1 = new Condition<Person>(p => p.Name, FilterOperators.Contains, "searchedValue1");
	var condition2 = new Condition<Person>(p => p.Name, FilterOperators.Contains, "searchedValue2");
	
	var orClause = new OrClause();
	orClause.Add(condition1, condition2);
	filter = orClause.ToLinq();
	// returns confition1 || condition2
	// filter = o => o.Name.Contains("searchedValue1") || o.Name.Contains("searchedValue2")
```	
### Complex And/Or clauses
Combinations of nested And / Or clauses can be created
Given 3 conditions :
``` csharp
	var people = new List<Person>();
	// Populate people
	var condition1 = new Condition<Person>(p => p.Name, FilterOperators.Contains, "searchedValue1");
	var condition2 = new Condition<Person>(p => p.Name, FilterOperators.Contains, "searchedValue2");
	var condition3 = new Condition<Person>(p => p.Address.City, FilterOperators.Contains, "searchedValue3");
```	
**And with nested Or**
```csharp
	var orClause = new OrClause();
	orClause.Add(condition1, condition2);

	var andClause = new andClause();
	andClause.Add(condition1);
	andclause.Add(orClause);
	
	filter = andClause.ToLinq();
	// returns condition3 && (condition1 || condition2)
	// filter = o => o.Address.City.Contains("searchedValue3") && (o.Name.Contains("searchedValue1") || o.Name.Contains("searchedValue2"))
```
**Or with nested And**
```csharp
	var andClause = new AndClause();
	AndClause.Add(condition1, condition2);

	var orClause = new orClause();
	andClause.Add(condition1);
	andclause.Add(andClause);
	
	filter = orClause.ToLinq();
	// returns condition3 || (condition1 && condition2)
	// filter = o => o.Address.City.Contains("searchedValue3") || (o.Name.Contains("searchedValue1") && o.Name.Contains("searchedValue2"))
```
