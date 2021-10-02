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
            "Claims" : null,
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
            "Claims" : null,
            "RefreshToken" : null,
            "CreationDate" : date,
            "UpdateDate" : date
        },
        {
            "_id" : UUID(),
            "Active" : true,
            "Email" : "john@queen.com",
            "Password" : "ukIHmXbeqEesKDhWpDdpfc7PZMjS2KMnXCd3ahJQvVsQpM0avk8GAcfEOALb1zh9/1yiSdm9TefdnTI+ArXcUA==",
            "Salt" : "/xywymSW9ly+Y56blVYbddLI8Be4zawrJJoeQY1fsL2TlF72FGOXK2mBH/zs3VdGo6+C58+a4PVRq9coP6aJWQ==",
            "Name" : "John",
            "LastName" : "Deacon",
            "Claims" : null,
            "RefreshToken" : null,
            "CreationDate" : date,
            "UpdateDate" : date
        },
        {
            "_id" : UUID(),
            "Active" : true,
            "Email" : "roger@queen.com",
            "Password" : "vIcSbGZAUJT60IS+DnZiPLpotHi6F8SKGcZMEPrCAxF3ebd0SYPFswIt5z/PgK7lK8xzZvGlywhnViFnzJ5j8w==",
            "Salt" : "i7LZY8Vp6f/0QZW8831fV8jNZYmErPwAi4YlVc3Sy8u+FoSE5ZPRi4Cr99f29VYHNvNZqUkwbOe1/PuUzPf/2w==",
            "Name" : "Roger",
            "LastName" : "Taylor",
            "Claims" : null,
            "RefreshToken" : null,
            "CreationDate" : date,
            "UpdateDate" : date
        }
    ])
}