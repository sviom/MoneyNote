import React, { Component } from 'react';

export class Counter extends Component {
    static displayName = Counter.name;

    constructor(props) {
        super(props);
        this.state = { currentCount: 0 };
        this.incrementCounter = this.incrementCounter.bind(this);
        this.state = { users: [], loading: true };
    }

    componentDidMount() {
        this.GetUserData();
    }

    incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }

    static RenderUserTable(users) {
        return (
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>½ÂÀÎµÊ</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(user =>
                        <tr key={user.id}>
                            <td>{user.id}</td>
                            <td>{user.isApproved}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Counter.RenderUserTable(this.state.users);

        return (
            <div>
                <h1>Counter</h1>

                <p>This is a simple example of a React component.</p>

                <p aria-live="polite">Current count: <strong>{this.state.currentCount}</strong></p>

                <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
                <p>»ç¿ëÀÚ Å×½ºÆ®</p>
                {contents}
            </div>
        );
    }

    async GetUserData() {
        const response = await fetch('user');
        const data = await response.json();
        console.log(data);
        this.setState({ users: data, loading: false });
    }
}
