export interface Activity { //the struct of our data =>no use IActivity because is the  convsion of c# and not of typescript
    id: string;
    title: string;
    date: string; //Date??
    description: string;
    category: string;
    city: string;
    venue: string;
}