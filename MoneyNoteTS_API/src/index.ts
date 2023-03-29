import express, { Response, Request } from 'express';

const app = express();
const port = 8080;

app.get('/', (req: Request, res: Response) => {
    res.send('test');
})


app.listen(port, () => {
    console.log('start server');
});