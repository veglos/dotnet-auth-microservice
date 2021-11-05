{
    db = db.getSiblingDB('auth_database');
    db.createCollection("Users");
    
    var date = ISODate();

    /*
    All passwords are p4$$w0rd
    */
    db.getCollection('Users').insert([
        {
            "_id" : UUID(),
            "Active" : true,
            "Email" : "freddie@queen.com",
            "Password" : "H+cwtkpByvIl5frKl3gslnFwXDkGu+nU0oJZruxrfQ3clT9mazn0WzMcWswFMv0MNcmrvtbgd0Km3RtbWO7LqA==",
            "Salt" : "hLT79dNxmKY8VGsen1R5tL+94ycNTFaFn+6bObKdvNvOgkoGOfZJoYBzZn7lNtuFZbG8Wn2UO4nULD185xdapA==",
            "Name" : "Freddie",
            "LastName" : "Mercury",
            "Claims": [{ "Type": "scope", "Value": "can_read_weather can_change_unit" }],
            "RefreshToken" : null,
            "CreationDate" : date,
            "UpdateDate" : date
        },
        {
            "_id" : UUID(),
            "Active" : true,
            "Email" : "brian@queen.com",
            "Password" : "K/7nypt56p2+xIsWQK4eQPvIAOaYTHs3YsvsVb+22dOWrGYMVvsu08nKoGyLejopzTmROQNKPhHmPttB0zeW0w==",
            "Salt" : "5y9alpf2gOIXSH5pF69mSXzXKJUh/Im/0hr7y4ipXCFumvfzPSDykS0g48ymEh/Fsf6tNW9RZX/2wZj09gbJNw==",
            "Name" : "Brian",
            "LastName" : "May",
            "Claims": null,
            "RefreshToken" : null,
            "CreationDate" : date,
            "UpdateDate" : date
        }
    ])
}