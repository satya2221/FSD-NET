import React, { Component } from 'react'
import {Link} from 'react-router-dom'

export default class ErrorBoundary extends Component {
    state = {hasError:false}
    static getDerivedStateFromError(){
        return{
            hasError:true
        }
    }
    componentDidCatch(error, info){
        console.error(`Telah ditemukan sebuah error`, error, info);
    }
    render() {
        if(this.state.hasError){
            return (
                <h2>
                    Telah ditemukan sebuah error.{''}
                    <Link to={'/'}>Klik disini untuk kembali ke beranda</Link>
                </h2>
            )
        }   
        return this.props.children
    }
}
