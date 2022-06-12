import React, { useState, useEffect } from 'react';
import axios from 'axios';

const HomePage = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const getPosts = async () => {
            const { data } = await axios.get('/api/LakewoodScoop/getNews');
            setPosts(data);
        }
        getPosts();
    }, [])

    const getPost = (p, k) => {
        return <div className="card border-light mb-3 w-50 p-3 jumbotron" key={k}>
            <div className="card-header bg-transparent border-dark"><a href={p.link} target='_blank'>{p.title}</a></div>
            <div className="card-body text-dark">
                <h5 className="card-title"><img src={p.imageUrl} /></h5>
                <p className="card-text">{p.text}</p>
            </div>
            <div className="card-footer bg-transparent border-dark">{p.comments}</div>
        </div>
    }

    return <div className="container mt-5">
        {posts && posts.map((p, k) => getPost(p, k))}
    </div>
}

export default HomePage;