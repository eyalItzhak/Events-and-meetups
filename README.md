

## About

An application with features of a social network in order to organize and manage meetings/events.

React 18, .NET 7, Mobx  are used in the project.


client side repository
[Links](https://github.com/eyalItzhak/Event-and-meetup-Client)

## Installation - Server 

Make sure that dotnet 7 is downloaded and installed on the computer.

Opening the terminal in the API folder and running the command:

```bash
dotnet watch run
```

You need to define a configuration file for connecting to cloudinary (for the image upload feature).

<img src=https://user-images.githubusercontent.com/62293316/209008740-d17612f7-e168-4001-aa96-11e535741629.png width=50% height=50%>

It is necessary to replace all the 'X' in your account details from the cloudinary website.


    
## Installation - Client 



client side repository
[Links](https://github.com/eyalItzhak/Event-and-meetup-Client)

Node.js 16.15.0 was used.

Open a terminal in the client-app folder and run the

```bash
npm install
```

command to download all the relevant files.

Then run

```bash
npm start
```


The client server side should open on port 3000.
## Deployment

The site is working and you can get an impression of it (link in releases).

Extra configuration was done beyond the git files in order to upload the website to fly.io such as switching from sqlite to postgresql and dockerizing the application.

The client side runs on the production build files.


#### to login:

Any of the following users are welcome to use:

bob@test.com

tom@test.com

jane@test.com

jerry@test.com

password=Pa$$w0rd



## Features

- User profile with the option to follow other users, upload photos and more.
- Live chat for the participants of the activity.
- Filters for finding the activity.
- Errors handler
- more...


## Screenshot
#### The opening page, when the user is not logged in is what he will see. Otherwise, he will be offered to go straight to the meeting list.

![image](https://user-images.githubusercontent.com/62293316/204667492-4e2020fe-f76b-436b-8a3b-1cf53f7ca973.png)

#### Registration and login forms

![image](https://user-images.githubusercontent.com/62293316/204667575-3a825774-8c9b-4b4e-84cc-bc14f78c04c1.png)
![image](https://user-images.githubusercontent.com/62293316/204667986-fa95bdea-8880-4a3e-8ce4-86a7e25cde5f.png)



#### main page
![image](https://user-images.githubusercontent.com/62293316/208905863-4d54e072-3c8f-4a2d-a3e8-06b6b39be958.png)

#### The meeting creation form.

![image](https://user-images.githubusercontent.com/62293316/208906517-7adbc86a-cbc2-4408-95b7-96a393f26ca5.png)

#### Meeting details and an option to edit an existing meeting

![image](https://user-images.githubusercontent.com/62293316/208906706-c3368ec1-964a-473f-bbce-8c217e5f88ab.png)

#### Error checking page => exists for testing

![image](https://user-images.githubusercontent.com/62293316/204668270-23106a0a-d29d-4e15-ae82-9d0f27a4fb28.png)








