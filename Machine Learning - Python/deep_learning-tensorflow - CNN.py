# MNIST For ML Beginners!

# This tutorial is intended for readers who are new to both machine learning and TensorFlow.
# Just like programming has Hello World, machine learning has MNIST.
# MNIST is a simple computer vision dataset. It consists of images of handwritten digits.
# Source: https://goo.gl/rLXVsR

# Please help us to improve this section by sending us your
# feedbacks and comments on: https://docs.google.com/forms/d/16fH20Qf8gJ2o31Vnlss2uLJ7wL9vq76TeUGqghTY0uI/viewform

###
# Follows is a more advanced implentation of tensorflow to get a higher accuracy than the previous machine learning script
# This actually gets a lower accuracy on codingame just because it timesout if you run the necessary # of training steps
###

def weight_variable(shape):
    # initialize a weight
    initial = tf.truncated_normal(shape, stddev=0.1)
    return tf.Variable(initial)

def bias_variable(shape):
    # initialize a bias
    initial = tf.constant(0.1, shape=shape)
    return tf.Variable(initial)
    
def conv2d(x, W):
    # https://www.youtube.com/watch?v=FmpDIaiMIeA
    # computes a 2-D convolusion given 4-D input tensor and 4-D filter tensor
    # https://www.tensorflow.org/api_docs/python/tf/nn/conv2d
    # this will return a 4-D tensor that is the same size as the input
    return tf.nn.conv2d(x, W, strides=[1, 1, 1, 1], padding='SAME')

def max_pool_2x2(x):
    # take a subsampling of 2x2 pixel windows
    # find the max of this 2x2 pixel window and let it represent the entire 2x2 window
    return tf.nn.max_pool(x, ksize=[1, 2, 2, 1], strides=[1, 2, 2, 1], padding='SAME')

# Importing input data
import random
import input_data
mnist = input_data.read_data_sets(raw_input(), raw_input(), raw_input())  

# inputs are:     
#   minst.train = data points to train the model 
#   minst.test = data points to test the model 
#   minst.validation =  data points for selecting hyper-parameters like learning rate and size of the model 
# data points formatted as:    minst.train.images, minst.train.labels
# images normalized to 28 x 28 pixels, flatten image for tensor length = 28*28 = 784

# predicted probabilities y = softmax(evidence of label) = softmax(Wx + b)
# x = tensor of image
# W = Weight
# b = bias

import tensorflow as tf
# implement first convolution layer: consisting of a convolution + max pooling
# layer will have 1 input and  compute 32 features, each feature being 5x5 pixels
# this will therefore the output will be 32 per 1 input
# BASIC CODE: W = tf.Variable(tf.zeros([784, 10]))
W_conv1 = weight_variable([5, 5, 1, 32]) # NEW CODE 

# implement a bia variable for the 32 output channels (was 10 in the basic code)
# BASIC CODE: b = tf.Variable(tf.zeros([10]))
b_conv1 = bias_variable([32])

# to apply the first convolution layer to the image, they must have the same tensor shape 
# (in this case both must be 4-D tensors)
# this operation reshapes the x input into a 4-D tensor of shape [-1, 28, 28, 1]
# -1 tells the function to automatically calculate the first dimension of the tensor
# 28, 28 is the image pixel dimensions, and 1 is the number of color channels
x = tf.placeholder(tf.float32, [None, 784])
x_image = tf.reshape(x, [-1, 28, 28, 1])

# produce model
# BASIC CODE: y = tf.nn.softmax(tf.matmul(x, W) + b)  
# convolve the x_image with the weight tensor and add the bias
# apply the ReLu function: basically takes a negative values and sets them to zero
# ReLu: (https://en.wikipedia.org/wiki/Rectifier_(neural_networks)
h_conv1 = tf.nn.relu(conv2d(x_image, W_conv1) + b_conv1)

# apply the 2x2 max pooling function
h_pool1 = max_pool_2x2(h_conv1)  # this reduces and outputs image of 14x14

# stack on another layer of convolution
# this layer has 64 features each 5x5
W_conv2 = weight_variable([5, 5, 32, 64])
b_conv2 = bias_variable([64])

h_conv2 = tf.nn.relu(conv2d(h_pool1, W_conv2) + b_conv2)
h_pool2 = max_pool_2x2(h_conv2)  # reduces and outputs image of 7x7

# add a "fully connected" layer - each value gives a vote to categorize the image as a handwritten 0-9
W_fc1 = weight_variable([7 * 7 * 64, 1024])
b_fc1 = bias_variable([1024])

h_pool2_flat = tf.reshape(h_pool2, [-1, 7 * 7 * 64])  # flatten to a 2-D tensor
h_fc1 = tf.nn.relu(tf.matmul(h_pool2_flat, W_fc1) + b_fc1)

# dropout - randomly drop some of the neurons and connections during training time to prevent overfitting
keep_prob = tf.placeholder(tf.float32)
h_fc1_drop = tf.nn.dropout(h_fc1, keep_prob)

# add readout layer, just like the softmax regression layer done in the basic code
W_fc2 = weight_variable([1024, 10])
b_fc2 = bias_variable([10])

y_conv = tf.matmul(h_fc1_drop, W_fc2)+b_fc2

# Model build END

# setup training 

# Use cross entropy to determine difference in the model's predicted probability distribution & actual probability distribution
# cross entropy = Hy'(y) = -Sum[y' * log(y)]  (will take mean of all the Hy'(y) )
# y = predicted probability distibution
# y' = y_ = actual probability distribution 
y_ = tf.placeholder(tf.float32, [None, 10])  # y' placeholder for actual probability distribution to fed in later

# BASIC CODE: cross_entropy = tf.reduce_mean(-tf.reduce_sum(y_ * tf.log(y), reduction_indices=[1]))
cross_entropy = tf.reduce_mean(tf.nn.softmax_cross_entropy_with_logits(labels=y_, logits=y_conv))  # basically a more stable way of doing the basic code just above

# define what happens each step of the training
# minimize the cross_entropy with an optimal learning rate of 0.05
# BASIC CODE: train_step = tf.train.GradientDescentOptimizer(0.05).minimize(cross_entropy)
train_step = tf.train.AdamOptimizer(1e-4).minimize(cross_entropy) # same as basic code but more sophisticated optimizer

correct_prediction = tf.equal(tf.argmax(y_conv, 1), tf.argmax(y_, 1)) # true if the largest value in both tensors are at the same spot
accuracy = tf.reduce_mean(tf.cast(correct_prediction, tf.float32))

# do the actual training: feed the model 100 of training set's images & labels, do it 1000 times
# BASIC CODE: 
# sess = tf.InteractiveSession()
# tf.initialize_all_variables().run() # initialize all the variables created above   (newer versions use: tf.global_variables_initializer().run())
# for _ in range(1000):
#    batch_images, batch_labels = mnist.train.next_batch(100)
#    sess.run(train_step, feed_dict={x: batch_images, y_: batch_labels})

# replacement for the basic code
with tf.Session() as sess:
    sess.run(tf.initialize_all_variables())
    for i in range(20):  # suggestion is to run this about 20000 times, but anything more than 20 timesout in codingame
        batch = mnist.train.next_batch(50)
        # if i % 100 == 0:
        #    train_accuracy = accuracy.eval(feed_dict={x: batch[0], y_: batch[1], keep_prob: 1.0})
        #    print ("Step: %d   Training Accuracy: %g" % (i, train_accuracy))
        train_step.run(feed_dict={x: batch[0], y_: batch[1], keep_prob: 0.5})
    # print("Test Accuracy: %g" % (accuracy.eval(feed_dict={x: mnist.test.images, y_: mnist.test.labels, keep_prob=1.0})))
    result = sess.run(tf.argmax(y_conv,1), feed_dict={x: mnist.validation.images, keep_prob: 1.0})


# print ' '.join(map(str, [random.randint(0,9) for _ in range(len(mnist.validation.images))]))  ignore codingame's random printing of numbers

print ' '.join(map(str, result))
#  This reaches a ~50% because of the timeout limit on codingame
