/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-noninteractive-element-interactions */
import { Component } from "react";

class Carousel extends Component{
    state = {
        active: 0
    }
    static defaultProps = {
        images: ['http://pets-images.dev-apis.com/pets/none.jpg']
    }
    handleIndexClick = (event) => {
        this.setState({
            active: +event.target.dataset.index
        })
    }
    render(){
        const {active} = this.state
        const {images} = this.props
        return(
            <div className="carousel">
                <img src={images[active]} alt="animal headline"  />
                <div className="carousel-smaller">
                    {images.map((photo, index) => (
                        <img 
                            key={index}
                            src={photo}
                            className={index === active ? 'active' : ''}
                            alt="animal small preview"
                            onClick={this.handleIndexClick}
                            data-index = {index}
                        />
                    ))}
                </div>
            </div>
        )
    }
}

export default Carousel