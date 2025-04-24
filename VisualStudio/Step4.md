# Step 4: Run basic query against own GraphQL Server

In this step you are going to query your own GraphQL Server. We do have to learn a bit about the GraphQL schema and query language to achieve this.

## Start your application
In [Step 3](Step3.md) you have configured the project to start the GraphQL endpoint. This endpoint does launch Nitro. This is a testbed for testing your API.

![Nitro](./images/Nitro.png)

To start creating a query, select the Create Document button.

## Gnerated schema

![Generated schema for query](./images/Schema%20main%20window.png)

![Generated schema for query method](./images/allOrders%20Schema.png)

![Generated schema for order](./images/Order%20schema.png)

## Create order query


```graphql
query {
  allOrders {
  }
}
```


```graphql
query {
  allOrders {
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


```

![Result for query](./images/Output%20first%20query.png)


