import React, { useState } from 'react';

const Generate = () => {

    const [amount, setAmount] = useState(0);

    const onGenerateClick = async () => {
        window.location.href = `/api/csvpeople/generatecsv/${amount}`; 
    }

    return (
        <div className='container'>
            <div className='d-flex vh-100'>
                <div className='d-flex w-100 justify-content-center align-self-center'>
                    <div className='row'>
                        <input type='text' className='form-control-lg' onChange={e => setAmount(e.target.value)} placeholder='Amount' />
                    </div>
                    <div className='row'>
                        <div className='col-md-4'>
                            <button className='btn btn-primary btn-lg' onClick={onGenerateClick} >Generate</button>
                        </div>
                    </div>
                </div>
            </div>
    </div>
        )
}

export default Generate;