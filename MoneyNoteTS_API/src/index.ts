import express, { Response, Request } from 'express';

const app = express();
const port = 8080;

app.get('/', (req: Request, res: Response) => {
    const test = req.query.test as string;
    if (test !== 'dfdfd') {
        console.log('tetete');
    }

    return res.send('test');
});

app.listen(port, () => {
    console.log('start server');
});
