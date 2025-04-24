# Step 6 Working with GraphQL fragments

In [step 4](./Step4.md) and [Step 5](./Step5.md) you have create a basic query. Both queries returned the same field and the definition for the fields was duplicated for one query to the next. This is of course not very maintainable. Fortunately, GraphQL has a solution for this in the form of fragments.

```graphql
fragment orderInfo on Order {
  id
  customer {
    name
  }
  orderStatus
  orderTime
  orderLines {
    product {
      name
    }
    quantity
  }
}
```

I did give the first query a name as well
```graphql
query getAllOrders {
  allOrders {
    ...orderInfo
  }
}

query getOrdersById($id: Int!) {
  orderById(id: $id){
    ...orderInfo
  }
}

```