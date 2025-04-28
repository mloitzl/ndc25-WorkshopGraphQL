# Step 5

Many API's need the ability to fetch an object by id. There are a few ways to achive this. In this step you wil extend your query class to support fetching a single order by id
GraphQL also supports passing parameters to a query. To demonstrate this, we are going to build a new query method to return an order based on it’s Id. This is just for demo purposes. In a real-life service, you would solve this in a different way. We will get to this later in the workshop.

## Update code

Add the following code to the OrderQuery class.
```csharp
public Order GetOrderById(int id)
{
    return GenerateTestOrders().Single(order => order.Id == id);
}
```
The Single ensures that only 1 record can ever be retrieved from the collection. Since the id field can be considered to be the primary key this will hold true for this workshop. You could of course also use the SingleOrDefault, to make sure that you can return an empty result if an id is passed that is not present in our collection. For a production API, I would really recommend this, but since this is a demmo project, just Single will do.

We do not need to change anything in program.cs for this to work. All we have to do is run the application again.

## Use the new method

If you look at the schema that is now generated, we you see that a new field has been added.


![Schema with query method with parameter added](./images/Query%20without%20external%20parameter.png)

We can use this field in the query as well. Add this query to your own test site. 

```graphql
query getOrdersById {
  orderById(id: 1){
    id
    customer {
      name
    }
    orderLines{
      product {
        name
      }
      quantity
    }
  }
}
```

An individual query can be run by clicking the Run button above the query.

![Run single query option 1](./images/Run%20single%20query.png)

You can also do this by selecting the query by choosing the correct one in the dropdown list next to the run button at the top of the pane.

![Run single query option 2](./images/Select%20individual%20query.pngq)

Run this query and verify that the result looks like this image.

![Query result](./images/Result%20query%20with%20parameter.png)


## Introduce parameter for GraphQL query

The parameter can be passed between the rounded brackets. This parameter is now fixed for this query, but we also have the option to add the parameter to the query itself, like in the code below.

```graphql
query getOrdersById($id: Int!) {
  orderById(id: $id){
    id
    customer {
      name
    }
    orderLines{
      product {
        name
      }
      quantity
    }
  }
}
```
Update your query to match the one above.

You can specify the parameter value to pass to the query, in the Variables pane.

![Query with parameter](./images/With%20passed%20parameter.png)

Run the query again and verify that the same result is produced.

In the [Next step](./Step6.md) you will remove the code duplication in the queries.

[Next step](./Step6.md)