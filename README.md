# SocialMediaDemo

## Overview
SocialMediaDemo is a project aimed at demonstrating the basic functionalities of a social media platform. This includes user registration, login, posting updates, liking and commenting on posts.

## Features
- User registration and authentication
- Create and edit posts
- Create edit and delete comments
- Like and unlike a posts
- Caching in All GET requests


## Installation
To get started with SocialMediaDemo, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/Mamoun-Hamed-EBLA/SocialMediaDemo.git
    ```

2. Navigate to the project directory:
    ```bash
    cd SocialMediaDemo
    ```

3. Update connection string (appsettings.json):
    ```appsettings.json
    "ConnectionStrings": {
       "DefaultConnection": "[Your connection string]"
   },
    ```

4. Update database (Packege Manager Consol):
    ```Open Packege Manager Consol
    set defualt project to infrastructure
    run : Update-Database
    ```
5. Run application (CMD):
    ```excute comma nd
    dotnet run
    ```

## Usage
Once the development server is running, you can access the application in your browser at `(https://localhost:7129;http://localhost:5025)`. From there, you can register a new account, log in, create posts, and explore the features of the platform.

## API Endpoints Here's a list of available API endpoints for SocialMediaDemo:

### Authentication 
- `POST /api/Auth/signup`: Register a new user
- `POST /api/Auth/login`: Log in a user

### Post 
- `GET /api/Post`: Get all posts
    - **Description**: Retrieves a list of all posts on the platform.
    - **Query Parameters**:
        - `Id`: (optional) Post id.
        - `UserId`: (optional) User id.
        - `MinCreatedAt`: (optional) Minimum post creating date.
        - `MaxCreatedAt`: (optional) Miximum post creating date.
        - `MinLastModified`: (optional) Minimum post updating date.
        - `MaxLastModified`: (optional) Miximum post updating date.
        - `OrderValue`: (optional) Take tow values (created,likes).
        - `isDescending`: (optional) Determain order behavior.
        - `PageNumber`: (optional) The page number for pagination.
        - `PageSize`: (optional) The number of posts per page.
- `POST /api/Post`: Create a new post
- `PUT /api/Post`: Update a post 
- `POST /api/Post/comment`: Create a new comment for a post
- `PUT /api/Post/like`: Like a post 
- `PUT /api/Post/unlike`: Unlike a post

### Comment 
- `GET /api/Comment`: Get all comments
    - **Description**: Retrieves a list of all comments on the platform.
    - **Query Parameters**:
        - `Id`: (optional) Comment id.
        - `PostId`: (optional) Post id.
        - `UserId`: (optional) User id.
        - `MinCreatedAt`: (optional) Minimum post creating date.
        - `MaxCreatedAt`: (optional) Miximum post creating date.
        - `MinLastModified`: (optional) Minimum post updating date.
        - `MaxLastModified`: (optional) Miximum post updating date.
        - `OrderValue`: (optional) Take one value (created).
        - `isDescending`: (optional) Determain order behavior.
        - `PageNumber`: (optional) The page number for pagination.
        - `PageSize`: (optional) The number of posts per page.
- `PUT /api/Comment`: Update a Comment 
- `DELETE /api/Comment`: Delete a Comment 

## Contributing
We welcome contributions to the project. If you'd like to contribute, please follow these steps:

1. Fork the repository
2. Create a new branch (`git checkout -b feature-branch`)
3. Make your changes and commit them (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature-branch`)
5. Open a pull request

## Contact
If you have any questions or feedback, please feel free to contact us at en15hamed1992@gmail.com.

