import React from 'react';
import { Route } from 'react-router';
import Layout from './Components/Layout';
import Home from './Pages/Home';
import Upload from './Pages/Upload';
import Generate from './Pages/Generate';

export default function App() {
    return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/upload' component={Upload} />
                <Route exact path='/generate' component={Generate} />
            </Layout>
    )
}