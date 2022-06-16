import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Home = () => {
    const [people, setPeople] = useState([]);

    useEffect(() => {
        const getPeople = async () => {
            const { data } = await axios.get('/api/csvpeople/getpeople');
            setPeople(data);
        }
        getPeople();
    });

    const onDeleteClick = async () => {
        await axios.post('/api/csvpeople/deleteall');
        setPeople([]);
    }

    return (<>
        <div className='container'>
            <div className='row'>
                <div className='col-md-6 offset-md-3 mt-5'>
                    <button className='btn btn-block btn-danger' onClick={onDeleteClick}>Delete All</button>
                </div>
            </div>
            <table className="table table-hover table-striped table-bordered mt-5">
                <thead>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Age</th>
                    <th>Address</th>
                    <th>Email</th>
                </thead>
                <tbody>
                    {people.map(({ id, firstName, lastName, age, email, address }) =>
                        <tr key={id}>
                            <td>{id}</td>
                            <td>{firstName}</td>
                            <td>{lastName}</td>
                            <td>{age}</td>
                            <td>{email}</td>
                            <td>{address}</td>
                        </tr>)}
                </tbody>
            </table>
        </div>
    </>
    )
}

export default Home;