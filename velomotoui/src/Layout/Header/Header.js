import React from 'react';
import styles from "./Header.module.css";
import CartButton from "./CartButton";

const Header = (props) => {
    return (
        <React.Fragment>
            <header className={styles.header}>
                <h1>Доставка Еды</h1>
            </header>
            <div className={styles['main-image']}>

            </div>
        </React.Fragment>
    );
};

export default Header;