# Step 5
In this step you wil extend your query class to support fetching a single order by id


```csharp
public Order GetOrderById(int id)
{
    return GenerateTestOrders().Single(order => order.Id == id);
}
```

![Schema with query method with parameter added](./images/Query%20without%20external%20parameter.png)

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


![Query with parameter](./images/With%20passed%20parameter.png)

