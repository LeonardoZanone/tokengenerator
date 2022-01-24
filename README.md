
## Token Generator

This project is a generator of tokens that will expire after 30 minutes.

To generate a token, a card number and costumer is needed.

**This is a demo project, and all data is being stored in memory**
## API Reference

#### Create card and get token

```http
  POST /api/v1/card
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `costumerId` | `int` | **Required**. The costumer Id |
| `cardNumber` | `long` | **Required**. The card number (16 characters) |
| `cvv` | `int` | **Required**. The cvv of the card (3 to 5 characters) |

#### Validate token

```http
  POST /api/v1/token
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `costumerId`      | `string` | **Required**. The costumer Id |
| `cardId`      | `string` | **Required**. The card Id |
| `token`      | `string` | **Required**. The token |
| `cvv`      | `string` | **Required**. The cvv of the card (3 to 5 characters) |


## Authors

- [@lzanone](https://www.github.com/LeonardoZanone)

