//its similar to interface but we can use it to create object also
export type User={
    id:string;
    displayName:string;
    email:string;
    token:string;
    imageUrl?:string;
}

export type UserCreds={
    email:string;
    password:string;
}

export type RegisterUser={
    displayName:string;
    email:string;
    password:string;
}