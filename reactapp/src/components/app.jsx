import React, { Suspense } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { TokenProvider } from 'context/token_context.jsx';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Header from 'components/header';
import Footer from 'components/footer';
import ErrorBoundary from 'components/error_boundary';
import {
    Staff, Feedback, Booking, Products, Auth, CustomerMain, TrainetMain,
    TicketMain, ServiceMain, StaffMain_m, CustomerMain_m, BookingMain, TLessonMain,
    UserMain, VisitMain, ScheduleMain, FeedbackMain
} from '../index'; 

import 'bootstrap/dist/css/bootstrap.min.css';
import '/src/css/style.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

const queryClient = new QueryClient();

const routes = [
    { path: '/', components: [Staff, Products, Booking, Feedback] },
    { path: '/auth', components: [Auth] },
    { path: '/admin', components: [VisitMain] },
    { path: '/customer', components: [CustomerMain] },
    { path: '/trainer', components: [TrainetMain] },
    { path: '/ticket', components: [TicketMain] },
    { path: '/service', components: [ServiceMain] },
    { path: '/staff_m', components: [StaffMain_m] },
    { path: '/customer_m', components: [CustomerMain_m] },
    { path: '/booking', components: [BookingMain] },
    { path: '/tlesson', components: [TLessonMain] },
    { path: '/user', components: [UserMain] },
    { path: '/visit', components: [VisitMain] },
    { path: '/schedule', components: [ScheduleMain] },
    { path: '/feedback', components: [FeedbackMain] },
];

const App = () => {
    return (
        <QueryClientProvider client={queryClient}>
            <TokenProvider>
                <Router>
                    <Header />
                    <div className="main-content">
                        <Suspense fallback={<div>Загрузка...</div>}>
                            <Routes>
                                {routes.map(({ path, components }) => (
                                    <Route
                                        key={path}
                                        path={path}
                                        element={
                                            <ErrorBoundary>
                                                {components.map((Comp, idx) => <Comp key={idx} />)}
                                            </ErrorBoundary>
                                        }
                                    />
                                ))}
                            </Routes>
                        </Suspense>
                    </div>
                    <Footer />
                </Router>
            </TokenProvider>
        </QueryClientProvider>
    );
};

export default App;