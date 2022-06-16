import axios from 'axios';
import React, { useState, useEffect, useRef } from 'react';
import { useHistory } from 'react-router-dom';

const Upload = () => {

    const fileInputRef = useRef(null);
    const history = useHistory();

    const toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });

    const onUploadClick = async () => {
        const file = fileInputRef.current.files[0];
        const base64 = await toBase64(file);
        console.log(base64);
        await axios.post('/api/csvpeople/upload', { Base64People: base64 });
        history.push('/');
    }

    return (<div className='container mt-5'>
        <div className='d-flex vh-100'>
            <div className='d-flex w-100 justify-content-center align-self-center'>
                <div className='row'>
                    <div className='col-md-10'>
                        <input ref={fileInputRef} type='file' className='form-control-lg' />
                    </div>
                    <div className='col-md-2'>
                        <button className='btn btn-primary btn-lg' onClick={onUploadClick}>Upload</button>
                    </div>
                </div>
            </div>
        </div>
    </div>)
}

export default Upload;