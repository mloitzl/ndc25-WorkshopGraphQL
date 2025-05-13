# Step 6: Working with GraphQL Fragments

In [Step 4](./Step4.md) and [Step 5](./Step5.md), you have created a basic query. Both queries returned the same fields, and the definition for the fields was duplicated from one query to the next. This is, of course, not very maintainable. Fortunately, GraphQL has a solution for this in the form of fragments.

When specifying a fragment you need to specify for which type the fragment should work by naming this type after the on keyword. The body of the fragment contains all the fields that you want returned.

Copy the following fragment to your request pane.

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

The fragment can now be used inside the query by using the `...` construction. In the code below, our first query is given a name now as well. Update your queries to match the ones below.

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

If you now run the queries, you should get the same results as before.

[Next step](./Step7.md)